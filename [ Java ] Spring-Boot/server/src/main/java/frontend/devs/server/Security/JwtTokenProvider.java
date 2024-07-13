package frontend.devs.server.Security;

import frontend.devs.server.Entities.CustomUserDetails;
import io.jsonwebtoken.*;
import io.jsonwebtoken.security.Keys;
import jakarta.servlet.http.HttpServletRequest;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Component;

import javax.crypto.SecretKey;
import java.util.Date;

@Component
public class JwtTokenProvider {

	private final SecretKey jwtSecret;

	@Value("${jwt.expiration}")
	private int jwtExpirationInMs;

	public JwtTokenProvider(@Value("${jwt.secret}") String secret) {
		this.jwtSecret = Keys.hmacShaKeyFor(secret.getBytes());
	}

	public String generateToken(String username, CustomUserDetails userDetails) {
		Date now = new Date();
		Date expiryDate = new Date(now.getTime() + jwtExpirationInMs);

		return Jwts.builder()
				  .claim("userId", userDetails.getId())///new
				  .setSubject(username)
				  .setIssuedAt(new Date())
				  .setExpiration(expiryDate)
				  .signWith(jwtSecret, SignatureAlgorithm.HS512)
				  .compact();
	}

	public String getUsernameFromToken(String token) {
		Claims claims = Jwts.parserBuilder()
				  .setSigningKey(jwtSecret)
				  .build()
				  .parseClaimsJws(token)
				  .getBody();

		return claims.getSubject();
	}

	// Trong lớp JwtTokenProvider
	public String getUserIdFromToken(String token) {
		Claims claims = Jwts.parserBuilder()
				  .setSigningKey(jwtSecret)
				  .build()
				  .parseClaimsJws(token)
				  .getBody();

		return claims.get("userId", String.class); // Giả sử ID người dùng được lưu trong claim "userId"
	}


	public boolean validateToken(String authToken) {
		try {
			Jwts.parserBuilder().setSigningKey(jwtSecret).build().parseClaimsJws(authToken);
			return true;
		} catch (SignatureException ex) {
			// log error
		} catch (MalformedJwtException ex) {
			// log error
		} catch (ExpiredJwtException ex) {
			// log error
		} catch (UnsupportedJwtException ex) {
			// log error
		} catch (IllegalArgumentException ex) {
			// log error
		}
		return false;
	}
}
