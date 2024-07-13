package frontend.devs.server.Util;

import frontend.devs.server.Entities.User;
import io.jsonwebtoken.Jwts;
import io.jsonwebtoken.SignatureAlgorithm;
import io.jsonwebtoken.security.Keys;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.stereotype.Component;

import java.util.Base64;
import java.util.Date;

@Component
public class JwtUtils
{

	public String generateToken(User user)
		{
			byte[] secretKeyBytes = Keys.secretKeyFor(SignatureAlgorithm.HS256).getEncoded();
			String secretString   = Base64.getEncoder().encodeToString(secretKeyBytes);

			String token = Jwts.builder().setSubject(user.getUserName())
//					              .claim("roles", user.getRoles())
					  .setIssuedAt(new Date()).setExpiration(new Date(System.currentTimeMillis() + 86400000))
					  .signWith(SignatureAlgorithm.HS256, secretString).compact();

			return token;
		}
}
