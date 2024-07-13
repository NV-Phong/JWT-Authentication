package frontend.devs.server.Controller;

import frontend.devs.server.RequestEntities.RequestUserUpdate;
import frontend.devs.server.Service.RoleService;
import frontend.devs.server.Service.UserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestMapping;

@Controller
@RequestMapping("/users")
public class UserController
{
	@Autowired
	private UserService userService;

	@Autowired
	private RoleService roleService;

	@GetMapping("")
	public String index(Model model)
		{
			model.addAttribute("users", userService.findAll());
			return "Layout/User/index";
		}

	@GetMapping("/edit/{id}")
	public String edit(@PathVariable String id, Model model)
		{
			model.addAttribute("user", userService.findById(id));
			model.addAttribute("roles", roleService.getAllRoles());
			return "Layout/User/edit";
		}

	@PostMapping("/savechange")
	public String saveChange(RequestUserUpdate requestUserUpdate)
		{
			userService.UpdateUser(requestUserUpdate);
			return "redirect:/users";
		}
}
