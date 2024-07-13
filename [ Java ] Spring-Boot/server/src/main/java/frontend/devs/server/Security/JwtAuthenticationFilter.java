package frontend.devs.server.Security;

import jakarta.servlet.FilterChain;
import jakarta.servlet.ServletException;
import jakarta.servlet.http.HttpServletRequest;
import jakarta.servlet.http.HttpServletResponse;
import lombok.RequiredArgsConstructor;
import org.springframework.security.authentication.UsernamePasswordAuthenticationToken;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.core.userdetails.UserDetailsService;
import org.springframework.stereotype.Component;
import org.springframework.web.filter.OncePerRequestFilter;

import java.io.IOException;

@Component
@RequiredArgsConstructor
public class JwtAuthenticationFilter extends OncePerRequestFilter
{

	private final JwtTokenProvider   tokenProvider;
	private final UserDetailsService userDetailsService;

	@Override
	protected void doFilterInternal(HttpServletRequest request, HttpServletResponse response, FilterChain filterChain)
			  throws ServletException, IOException
		{

			String token = getJwtFromRequest(request);

			if (token != null && tokenProvider.validateToken(token))
				{
					String      username    = tokenProvider.getUsernameFromToken(token);
					UserDetails userDetails = userDetailsService.loadUserByUsername(username);
					UsernamePasswordAuthenticationToken authentication = new UsernamePasswordAuthenticationToken(userDetails,
							  null, userDetails.getAuthorities());
					SecurityContextHolder.getContext().setAuthentication(authentication);
				}
			filterChain.doFilter(request, response);
		}

	public String getJwtFromRequest(HttpServletRequest request)
		{
			String bearerToken = request.getHeader("Authorization");
			if (bearerToken != null && bearerToken.startsWith("Bearer "))
				{
					return bearerToken.substring(7);
				}
			return null;
		}
}
