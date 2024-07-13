package frontend.devs.server.Repositories;

import frontend.devs.server.Entities.Collection;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;

import java.util.List;
import java.util.Optional;

@Repository
public interface CollectionRepository extends JpaRepository<Collection, String>
{
	@Query("SELECT u FROM Collection u WHERE u.user.IDUser = :IDUser")
	List<Collection> findByIDUser(@Param("IDUser") String IDUser);

	@Query("SELECT u FROM Collection u WHERE u.name = :name")
	Optional<Collection> findByName(@Param("name") String name);
}
