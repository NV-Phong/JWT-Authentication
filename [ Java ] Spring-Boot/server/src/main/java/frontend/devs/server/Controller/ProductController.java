package frontend.devs.server.Controller;

import frontend.devs.server.Entities.Product;
import frontend.devs.server.RequestEntities.CreateProduct;
import frontend.devs.server.Service.ProductService;
import jakarta.servlet.http.HttpServletRequest;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("Products")
public class ProductController
{
	@Autowired
	private ProductService productService;

	@GetMapping
	public ResponseEntity<?> getAllbyIsprivate()
		{
			List<Product> products = productService.findAllbyIsprivate();
			if (products != null)
				{
					return ResponseEntity.status(200).body(products);
				}
			else
				{
					return ResponseEntity.badRequest().body(null);
				}
		}

	@PostMapping
	public ResponseEntity<?> addProduct(@RequestBody CreateProduct product, HttpServletRequest request)
		{
			Product listproduct = productService.CreateProduct(product, request);
			if (listproduct != null)
				{
					return ResponseEntity.status(HttpStatus.CREATED).body(listproduct);
				}
			else
				{
					return ResponseEntity.badRequest().body(null);
				}
		}

}
