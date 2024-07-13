package frontend.devs.server.Repositories;

import frontend.devs.server.Entities.CRUD;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import java.util.List;


@Repository
public interface CRUDRepository extends JpaRepository<CRUD, String>
{
   List<CRUD> findByCRUDName(String cRUDName);
}

