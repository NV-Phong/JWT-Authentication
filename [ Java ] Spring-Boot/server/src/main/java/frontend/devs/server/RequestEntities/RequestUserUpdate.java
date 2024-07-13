package frontend.devs.server.RequestEntities;


import frontend.devs.server.Entities.Role;
import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

@Getter
@Setter
@NoArgsConstructor
@AllArgsConstructor
public class RequestUserUpdate {
    private String id;
    private String password;
    private String email;
    private String display_name;
    private String cover;
    private Role role;
}