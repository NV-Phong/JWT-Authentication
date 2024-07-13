package frontend.devs.server.Repositories;

import frontend.devs.server.Entities.Collection;
import frontend.devs.server.Entities.Contact;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;

import java.util.List;

@Repository
public interface ContactRepository extends JpaRepository<Contact, String>
{
	@Query("SELECT u FROM Contact u WHERE u.user.IDUser = :IDUser")
	List<Contact> findByIDUser(@Param("IDUser") String IDUser);

}
