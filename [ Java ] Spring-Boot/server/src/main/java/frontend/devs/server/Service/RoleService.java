package frontend.devs.server.Service;

import frontend.devs.server.Entities.Role;
import frontend.devs.server.Repositories.RoleRepository;
import frontend.devs.server.RequestEntities.RequestRole;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class RoleService
{
	@Autowired
	private RoleRepository roleRepository;

	public List<Role> getAllRoles()
		{
			return roleRepository.findAll();
		}

	public Role getRoleById(String id)
		{
			return roleRepository.findById(id).get();
		}

	public Role createRole(RequestRole requestRole)
		{
			Role role = new Role();
			role.setRoleName(requestRole.getRole_name());
			return roleRepository.save(role);
		}
}
