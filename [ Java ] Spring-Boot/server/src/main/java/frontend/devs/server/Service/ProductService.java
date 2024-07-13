package frontend.devs.server.Service;

import frontend.devs.server.Entities.Product;
import frontend.devs.server.Entities.User;
import frontend.devs.server.Repositories.CollectionRepository;
import frontend.devs.server.Repositories.ProductRepository;
import frontend.devs.server.Repositories.UserRepository;
import frontend.devs.server.RequestEntities.CreateProduct;
import frontend.devs.server.Security.JwtAuthenticationFilter;
import frontend.devs.server.Security.JwtTokenProvider;
import jakarta.servlet.http.HttpServletRequest;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.Collection;
import java.util.List;
import java.util.stream.Collectors;

@Service
public class ProductService
{
	@Autowired
	private ProductRepository productRepository;

	@Autowired
	private JwtAuthenticationFilter _JwtAuthenticationFilter;

	@Autowired
	private JwtTokenProvider _JwtTokenProvider;

	@Autowired
	private CollectionService _CollectionService;

	@Autowired
	private UserRepository _UserRepository;

	@Autowired
	private CollectionRepository _CollectionRepository;

	public List<Product> findAllbyIsprivate()
		{
			List<Product> products = productRepository.findAll();

			return products.stream()
					  .filter(p -> !p.isIsprivate())
					  .collect(Collectors.toList());

		}

	public List<Product> findByIDUser(HttpServletRequest request)
		{
			String token = _JwtAuthenticationFilter.getJwtFromRequest(request);
			if (token != null && _JwtTokenProvider.validateToken(token))
				{
					String userId = _JwtTokenProvider.getUserIdFromToken(token);
					return productRepository.findByIDUser(userId);
				}
			return null;

		}

	public Product CreateProduct(CreateProduct product, HttpServletRequest request)
		{
			Product saveProduct = new Product();
			String token = _JwtAuthenticationFilter.getJwtFromRequest(request);
			if (token != null && _JwtTokenProvider.validateToken(token))
				{
					String userId = _JwtTokenProvider.getUserIdFromToken(token);
					User   user   = _UserRepository.findById(userId).orElseThrow(() -> new RuntimeException("User not found"));
					saveProduct.setCollection(_CollectionRepository.findByName(product.getCollectionname()).orElseThrow());
					saveProduct.setUser(user);
					saveProduct.setPrice(product.getPrice());
					saveProduct.setName(product.getName());
					saveProduct.setDescription(product.getDescription());
					saveProduct.setIsprivate(product.isIsprivate());

				}

			return productRepository.save(saveProduct);
		}


}
