package frontend.devs.server.Service;

import frontend.devs.server.Entities.User;
import frontend.devs.server.Repositories.UserRepository;
import frontend.devs.server.RequestEntities.RequestUserUpdate;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class UserService
{
	private final UserRepository _UserRepository;

	public UserService(UserRepository _UserRepository)
		{
			this._UserRepository = _UserRepository;
		}

	public List<User> findAll()
		{
			return _UserRepository.findAll();
		}

	public User findById(String id)
		{
			return _UserRepository.findById(id).get();
		}

	public User FindUserByUserName(String UserName)
		{
			return _UserRepository.findByUserName(UserName);
		}

	public User SaveUser(User user)
		{
			user.setPassword(new BCryptPasswordEncoder().encode(user.getPassword()));
			_UserRepository.save(user);
			return user;
		}

	public User UpdateUser(RequestUserUpdate requestUserUpdate)
		{
			try
				{
					User user = _UserRepository.findById(requestUserUpdate.getId()).get();
					user.setPassword(requestUserUpdate.getPassword());
					user.setEmail(requestUserUpdate.getEmail());
					user.setRole(requestUserUpdate.getRole());
					return _UserRepository.save(user);
				}
			catch (Exception e)
				{
					throw new RuntimeException(e.getMessage());
				}
		}

}
