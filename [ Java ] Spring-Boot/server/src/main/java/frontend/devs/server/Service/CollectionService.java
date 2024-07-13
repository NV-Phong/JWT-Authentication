package frontend.devs.server.Service;

import frontend.devs.server.Entities.Collection;
import frontend.devs.server.Entities.User;
import frontend.devs.server.Repositories.CollectionRepository;
import frontend.devs.server.Repositories.UserRepository;
import frontend.devs.server.Security.JwtAuthenticationFilter;
import frontend.devs.server.Security.JwtTokenProvider;
import jakarta.servlet.http.HttpServletRequest;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import java.util.List;

@Service
public class CollectionService
{
	@Autowired
	private CollectionRepository collectionRepository;

	@Autowired
	private JwtAuthenticationFilter _JwtAuthenticationFilter;

	@Autowired
	private JwtTokenProvider _JwtTokenProvider;

	@Autowired
	private UserRepository _UserRepository;

	public List<Collection> findAll()
		{
			return collectionRepository.findAll();
		}

	public List<Collection> findByID(HttpServletRequest request)
		{
			String token = _JwtAuthenticationFilter.getJwtFromRequest(request);
			if (token != null && _JwtTokenProvider.validateToken(token))
				{
					String userId = _JwtTokenProvider.getUserIdFromToken(token);
					return collectionRepository.findByIDUser(userId);
				}
			return null;

		}


	public Collection CreateCollection(Collection collection, HttpServletRequest request)
		{
			String token = _JwtAuthenticationFilter.getJwtFromRequest(request);
			if (token != null && _JwtTokenProvider.validateToken(token))
				{
					String         userId = _JwtTokenProvider.getUserIdFromToken(token);
					User user = _UserRepository.findById(userId).orElseThrow(() -> new RuntimeException("User not found"));
					collection.setUser(user);
				}

			return collectionRepository.save(collection);
		}

}
