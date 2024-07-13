package frontend.devs.server.Controller;

import frontend.devs.server.Entities.Contact;
import frontend.devs.server.Service.ContactService;
import jakarta.servlet.http.HttpServletRequest;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("Contact")
public class ContactController
{
	@Autowired
	private ContactService contactService;

	@GetMapping("GetAll")
	public ResponseEntity<?> getAllContacts()
		{
			List<Contact> contacts = contactService.findAll();
			if (contacts != null)
				{
					return ResponseEntity.status(200).body(contacts);
				}
			else
				{
					return ResponseEntity.badRequest().body(null);
				}
		}

	@GetMapping
	public ResponseEntity<?> getContactbyUser(HttpServletRequest request)
		{
			List<Contact> contacts = contactService.findContactbyIDUser(request);
			if (contacts != null)
				{
					return ResponseEntity.status(200).body(contacts);
				}
			else
				{
					return ResponseEntity.badRequest().body(null);
				}

		}

	@PostMapping
	public ResponseEntity<?> addContact(@RequestBody Contact contact, HttpServletRequest request)
		{
			Contact CreateContacts = contactService.CreateContact(contact, request);
			if (CreateContacts != null)
				{
					return ResponseEntity.status(HttpStatus.CREATED).body(CreateContacts);
				}
			else
				{
					return ResponseEntity.badRequest().body(null);
				}
		}
}
