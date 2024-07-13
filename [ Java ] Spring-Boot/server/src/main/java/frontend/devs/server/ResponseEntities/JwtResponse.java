package frontend.devs.server.ResponseEntities;

import lombok.Data;

@Data
public class JwtResponse
{
	private String Token;
	private String RefreshToken;

	public JwtResponse(String jwt)
		{
			this.Token = jwt;
		}
}
