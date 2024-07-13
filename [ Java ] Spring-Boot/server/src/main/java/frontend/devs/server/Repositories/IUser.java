package frontend.devs.server.Repositories;

import frontend.devs.server.Entities.User;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;

import java.util.Optional;

@Repository
public interface IUser extends JpaRepository<User, String>
{
	@Query("SELECT u FROM User u WHERE u.UserName = :UserName")
	Optional<User> findByUserName(@Param("UserName") String UserName);

}
