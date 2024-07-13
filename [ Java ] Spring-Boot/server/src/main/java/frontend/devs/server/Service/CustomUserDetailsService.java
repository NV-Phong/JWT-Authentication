package frontend.devs.server.Service;

import frontend.devs.server.Entities.CustomUserDetails;
import frontend.devs.server.Entities.User;
import frontend.devs.server.Repositories.IUser;
import lombok.AllArgsConstructor;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.core.userdetails.UserDetailsService;
import org.springframework.security.core.userdetails.UsernameNotFoundException;
import org.springframework.stereotype.Service;

@AllArgsConstructor
@Service
public class CustomUserDetailsService implements UserDetailsService
{
	private IUser _IUser;

	@Override
	public UserDetails loadUserByUsername(String username) throws UsernameNotFoundException
		{
			User user = _IUser.findByUserName(username)
					  .orElseThrow(() -> new UsernameNotFoundException("User not found with username: " + username));

			return new CustomUserDetails(user);
		}
}
