package frontend.devs.server.Service;

import frontend.devs.server.Entities.Collection;
import frontend.devs.server.Entities.Contact;
import frontend.devs.server.Entities.User;
import frontend.devs.server.Repositories.ContactRepository;
import frontend.devs.server.Repositories.UserRepository;
import frontend.devs.server.Security.JwtAuthenticationFilter;
import frontend.devs.server.Security.JwtTokenProvider;
import jakarta.servlet.http.HttpServletRequest;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class ContactService
{
	@Autowired
	private ContactRepository       contactRepository;

	@Autowired
	private JwtAuthenticationFilter _JwtAuthenticationFilter;

	@Autowired
	private JwtTokenProvider _JwtTokenProvider;

	@Autowired
	private UserRepository _UserRepository;

	public List<Contact> findContactbyIDUser(HttpServletRequest request)
		{
			String token = _JwtAuthenticationFilter.getJwtFromRequest(request);
			if (token != null && _JwtTokenProvider.validateToken(token))
				{
					String userId = _JwtTokenProvider.getUserIdFromToken(token);
					return contactRepository.findByIDUser(userId);
				}
			return null;
		}

	public Contact CreateContact(Contact contact, HttpServletRequest request)
		{
			String token = _JwtAuthenticationFilter.getJwtFromRequest(request);
			if (token != null && _JwtTokenProvider.validateToken(token))
				{
					String userId = _JwtTokenProvider.getUserIdFromToken(token);
					User   user   = _UserRepository.findById(userId).orElseThrow(() -> new RuntimeException("User not found"));
					contact.setUser(user);
				}

			return contactRepository.save(contact);
		}
	public List<Contact> findAll()
		{
			return contactRepository.findAll();
		}
}
