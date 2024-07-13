package frontend.devs.server.RequestEntities;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.math.BigDecimal;

@Data
@NoArgsConstructor
@AllArgsConstructor
public class CreateProduct {
    private String name;
    private String description;
    private String image;
    private BigDecimal price;
    private boolean isprivate;
    private String collectionname;
}