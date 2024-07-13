package frontend.devs.server.Repositories;

import frontend.devs.server.Entities.Collection;
import frontend.devs.server.Entities.Product;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;

import java.util.List;
import java.util.Optional;

@Repository
public interface ProductRepository extends JpaRepository<Product, String>
{

	Optional<Product> findById(String id);

	@Query("SELECT u FROM Product u WHERE u.user.IDUser = :IDUser")
	List<Product> findByIDUser(@Param("IDUser") String IDUser);

	@Query("SELECT u FROM Product u WHERE u.collection.id = :id")
	List<Product> findByIDCollection(@Param("id") String id);



}
