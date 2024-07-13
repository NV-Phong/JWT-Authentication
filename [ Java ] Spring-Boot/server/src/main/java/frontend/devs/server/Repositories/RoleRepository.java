package frontend.devs.server.Repositories;

import frontend.devs.server.Entities.Role;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

public interface RoleRepository extends JpaRepository<Role, String>
{
	//	@Query("SELECT u FROM Role u WHERE u.RoleName = :RoleName")
	//	boolean existsByName(@Param("RoleName") String RoleName);
}
