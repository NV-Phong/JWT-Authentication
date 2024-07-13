package frontend.devs.server.Entities;

import com.fasterxml.jackson.annotation.JsonProperty;
import jakarta.persistence.*;
import lombok.Data;

import java.util.List;

@Entity
@Data
@Table(name = "Role")
public class Role
{
	@Id
	@JsonProperty("IDRole")
	@GeneratedValue(strategy = GenerationType.UUID)
	private String IDRole;

	@JsonProperty("RoleName")
	@Column(unique = true, nullable = false)
	private String RoleName;

	@OneToMany(mappedBy = "Role")
	private List<User> users;
}
