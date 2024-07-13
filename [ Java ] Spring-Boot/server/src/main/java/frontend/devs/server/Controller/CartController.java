package frontend.devs.server.Controller;

import frontend.devs.server.Entities.Cart;
import frontend.devs.server.RequestEntities.Order;
import frontend.devs.server.Service.CartService;
import jakarta.servlet.http.HttpServletRequest;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("Cart")
public class CartController
{
	@Autowired
	private CartService cartService;

	@PostMapping
	public ResponseEntity<?> addProductToCart(@RequestBody Order order, HttpServletRequest request)
		{
			Cart cart = cartService.CreateCart(order, request);
			if (cart != null)
				{
					return ResponseEntity.status(HttpStatus.CREATED).body(cart);
				}
			else
				{
					return ResponseEntity.badRequest().body(null);
				}
		}

	@GetMapping
	public ResponseEntity<?> getCart(HttpServletRequest request)
		{
			List<Cart> carts = cartService.findByIDUser(request);
			if (carts != null)
				{
					return ResponseEntity.status(200).body(carts);
				}
			else
				{
					return ResponseEntity.badRequest().body(null);
				}
		}

	@PostMapping("ClearCart")
	public ResponseEntity<?> clearCart(HttpServletRequest request)
		{
			List<Cart> carts = cartService.clearCart(request);
			if (carts != null)
				{
					return ResponseEntity.status(200).body(carts);
				}
			else
				{
					return ResponseEntity.badRequest().body(null);
				}
		}
}
