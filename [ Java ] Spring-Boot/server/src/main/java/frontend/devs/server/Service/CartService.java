package frontend.devs.server.Service;

import frontend.devs.server.Entities.Cart;
import frontend.devs.server.Entities.Collection;
import frontend.devs.server.Entities.Product;
import frontend.devs.server.Entities.User;
import frontend.devs.server.Repositories.CartRepository;
import frontend.devs.server.Repositories.ProductRepository;
import frontend.devs.server.Repositories.UserRepository;
import frontend.devs.server.RequestEntities.Order;
import frontend.devs.server.Security.JwtAuthenticationFilter;
import frontend.devs.server.Security.JwtTokenProvider;
import jakarta.servlet.http.HttpServletRequest;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.Date;
import java.util.List;

@Service
public class CartService
{
	@Autowired
	private CartRepository cartRepository;

	@Autowired
	private JwtAuthenticationFilter _JwtAuthenticationFilter;

	@Autowired
	private JwtTokenProvider _JwtTokenProvider;

	@Autowired
	private UserRepository _UserRepository;

	@Autowired
	private ProductRepository productRepository;

	public Cart CreateCart(Order cart, HttpServletRequest request)
		{
			Cart saveCart = new Cart();
			String token = _JwtAuthenticationFilter.getJwtFromRequest(request);
			if (token != null && _JwtTokenProvider.validateToken(token))
				{
					String userId = _JwtTokenProvider.getUserIdFromToken(token);
					User   user   = _UserRepository.findById(userId).orElseThrow(() -> new RuntimeException("User not found"));
					saveCart.setUser(user);
					saveCart.setDay_create();
					saveCart.setProduct(productRepository.findById(cart.getIdproduct()).orElseThrow());
				}

			return cartRepository.save(saveCart);
		}

	public List<Cart> findByIDUser(HttpServletRequest request)
		{
			String token = _JwtAuthenticationFilter.getJwtFromRequest(request);
			if (token != null && _JwtTokenProvider.validateToken(token))
				{
					String userId = _JwtTokenProvider.getUserIdFromToken(token);
					return cartRepository.findByIDUser(userId);
				}
			return null;
		}

	public List<Cart> clearCart(HttpServletRequest request)
		{
			String token = _JwtAuthenticationFilter.getJwtFromRequest(request);
			if (token != null && _JwtTokenProvider.validateToken(token))
				{
					String userId = _JwtTokenProvider.getUserIdFromToken(token);
					List<Cart> carts = cartRepository.findByIDUser(userId);
					carts.clear();
				}
			return null;
		}

}
