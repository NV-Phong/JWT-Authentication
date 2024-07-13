package frontend.devs.server.Security;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public enum Role
{
	ADMIN("ADMIN"), USER("USER");
	public final String RoleName;

}
