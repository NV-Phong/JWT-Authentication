package frontend.devs.server.Controller;

import frontend.devs.server.Entities.CRUD;
import frontend.devs.server.Repositories.CRUDRepository;
import lombok.AllArgsConstructor;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@AllArgsConstructor
@RequestMapping("/CRUD")
public class CRUDController
{
	private CRUDRepository _CRUDRepository;

	@GetMapping
	public List<CRUD> GetCRUD()
		{
			return _CRUDRepository.findAll();
		}

	@PostMapping
	public CRUD CreateCRUD(@RequestBody CRUD crud)
		{
			return _CRUDRepository.save(crud);
		}

}

