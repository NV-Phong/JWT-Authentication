package frontend.devs.server.Controller;

import frontend.devs.server.Entities.Role;
import frontend.devs.server.RequestEntities.RequestRole;
import frontend.devs.server.Service.RoleService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/Roles")
public class RoleController
{
	@Autowired
	private RoleService roleService;

	@PostMapping
	public ResponseEntity<?> CreateRole(@RequestBody RequestRole requestRole)
		{
			Role savedRole = roleService.createRole(requestRole);
			if (savedRole != null)
				{
					return ResponseEntity.status(HttpStatus.CREATED).body(savedRole);
				}
			else
				{
					return ResponseEntity.badRequest().build();
				}
		}

	@GetMapping
	public ResponseEntity<?> GetRole()
		{
			List<Role> listRole = roleService.getAllRoles();
			if (listRole != null)
				{
					return ResponseEntity.status(200).body(listRole);
				}
			else
				{
					return ResponseEntity.badRequest().build();
				}
		}
}