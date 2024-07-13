package frontend.devs.server.Controller;

import frontend.devs.server.Entities.CustomUserDetails;
import frontend.devs.server.ResponseEntities.JwtResponse;
import frontend.devs.server.Entities.User;
import frontend.devs.server.Security.JwtTokenProvider;
import frontend.devs.server.Service.UserService;
import jakarta.servlet.http.Cookie;
import jakarta.servlet.http.HttpServletResponse;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.web.bind.annotation.*;

@CrossOrigin("http://localhost:4321")
@RestController
@RequestMapping("/Auth")
public class AuthController
{
	@Autowired
	private UserService _UserService;

	@Autowired
	private BCryptPasswordEncoder _BCryptPasswordEncoder;

	@Autowired
	private JwtTokenProvider _JwtTokenProvider;

	@PostMapping("Register")
	public ResponseEntity<User> Register(@RequestBody User user)
		{
			User savedUser = _UserService.SaveUser(user);
			if (savedUser != null)
				{
					return ResponseEntity.status(HttpStatus.CREATED).body(savedUser);
				}
			else
				{
					return ResponseEntity.badRequest().build();
				}
		}

	@PostMapping("Login")
	public ResponseEntity<JwtResponse> Login(@RequestBody User credentials, HttpServletResponse response)
		{
			User user = _UserService.FindUserByUserName(credentials.getUserName());

			if (user != null && _BCryptPasswordEncoder.matches(credentials.getPassword(), user.getPassword()))
				{
					CustomUserDetails userDetails = new CustomUserDetails(user);
					String            token       = _JwtTokenProvider.generateToken(user.getUserName(), userDetails);

					Cookie cookie = new Cookie("token", token);
					cookie.setHttpOnly(false);
					cookie.setPath("/");
					response.addCookie(cookie);
					return ResponseEntity.ok(new JwtResponse(token));
				}
			else
				{
					return ResponseEntity.status(HttpStatus.UNAUTHORIZED).body(new JwtResponse(null));
				}
		}

	@PostMapping("Logout")
	public ResponseEntity<?> logout(HttpServletResponse response)
		{
			Cookie cookie = new Cookie("token", null);
			cookie.setMaxAge(0);
			cookie.setPath("/");
			response.addCookie(cookie);

			return ResponseEntity.ok().build();
		}

}
