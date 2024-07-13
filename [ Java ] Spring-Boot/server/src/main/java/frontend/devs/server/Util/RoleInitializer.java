package frontend.devs.server.Util;

import frontend.devs.server.Entities.Role;
import frontend.devs.server.Repositories.RoleRepository;
import jakarta.annotation.PostConstruct;
import lombok.ToString;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

@Component
public class RoleInitializer
{

	@Autowired
	private RoleRepository roleRepository;

	@PostConstruct
	public void init()
		{
//			for (frontend.devs.server.Security.Role securityRole : frontend.devs.server.Security.Role.values()) {
//				String roleName = String.valueOf(securityRole.getRoleName());
//				if (roleRepository.existsByName(roleName)) {
//					Role entityRole = new Role();
//					entityRole.setRoleName(roleName);
//					roleRepository.save(entityRole);
//				}
//			}
		}
}
