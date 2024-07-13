package frontend.devs.server.Entities;

import com.fasterxml.jackson.annotation.JsonProperty;
import jakarta.persistence.*;
import jakarta.validation.constraints.*;
import lombok.Data;

@Entity
@Data
public class User
{
	@Id
	@JsonProperty("IDUser")
	@GeneratedValue(strategy = GenerationType.UUID)
	private String IDUser;

	@JsonProperty("UserName")
	@Column(unique = true, nullable = false)
	@NotBlank(message = "Username is required")
	private String UserName;

	@JsonProperty("Email")
	@Column(unique = true, nullable = false)
	@Email
	@NotBlank(message = "Email is required")
	private String Email;

	@JsonProperty("Password")
	@NotBlank(message = "Password is required")
	private String Password;

	@JsonProperty("DisplayName")
	private String DisplayName;

	@JsonProperty("Avatar")
	private String Avatar;

	@JsonProperty("Cover")
	private String Cover;

	@JsonProperty("Provider")
	private String Provider;

	@JsonProperty("IsDeleted")
	private Boolean IsDeleted;

	@ManyToOne
	@JoinColumn(name = "IDRole")
	private Role Role;


}
