package frontend.devs.server.Entities;

import com.fasterxml.jackson.annotation.JsonProperty;
import jakarta.annotation.security.PermitAll;
import jakarta.persistence.*;
import lombok.Data;

@Entity
@Table(name = "CRUD")
@Data
@PermitAll
public class CRUD {
	@Id
	@JsonProperty("IDCRUD")
	@GeneratedValue(strategy = GenerationType.UUID)
	private String IDUser;

	@JsonProperty("CRUDName")
	@Column(nullable = false)
	private String CRUDName;

	@JsonProperty("Email")
	@Column(nullable = false, unique = true)
	private String Email;


}

