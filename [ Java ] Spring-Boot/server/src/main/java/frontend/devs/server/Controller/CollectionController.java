package frontend.devs.server.Controller;

import frontend.devs.server.Entities.Collection;
import frontend.devs.server.Service.CollectionService;
import jakarta.servlet.http.HttpServletRequest;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("Collection")
public class CollectionController
{
	@Autowired
	private CollectionService collectionService;

	@GetMapping("GetAll")
	public ResponseEntity<?> getAllCollections()
		{
			List<Collection> collectionList = collectionService.findAll();
			if (collectionList != null)
				{
					return ResponseEntity.status(200).body(collectionList);
				}
			else
				{
					return ResponseEntity.badRequest().body(null);
				}
		}

	@GetMapping
	public ResponseEntity<?> getCollectionbyUser(HttpServletRequest request)
		{
			List<Collection> collectionList = collectionService.findByID(request);
			if (collectionList != null)
				{
					return ResponseEntity.status(200).body(collectionList);
				}
			else
				{
					return ResponseEntity.badRequest().body(null);
				}

		}

	@PostMapping
	public ResponseEntity<?> addCollection(@RequestBody Collection collection, HttpServletRequest request)
		{
			Collection CreateCollections = collectionService.CreateCollection(collection, request);
			if (CreateCollections != null)
				{
					return ResponseEntity.status(HttpStatus.CREATED).body(CreateCollections);
				}
			else
				{
					return ResponseEntity.badRequest().body(null);
				}
		}

}
