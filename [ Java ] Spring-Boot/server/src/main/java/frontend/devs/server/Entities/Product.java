package frontend.devs.server.Entities;

import jakarta.persistence.*;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.math.BigDecimal;

@Entity
@Data
@NoArgsConstructor
@AllArgsConstructor
public class Product
{
    @Id
    @GeneratedValue(strategy = GenerationType.UUID)
    private String id;
    private String name;
    private String description;
    private String image;
    private BigDecimal price;
    private boolean isprivate;
    private boolean is_delete;

    @ManyToOne
    @JoinColumn(name = "IDUser")
    private User user;

    @ManyToOne
    @JoinColumn(name = "collection_id")
    private Collection collection;
}
