package frontend.devs.server.Security;

import org.springframework.context.annotation.Configuration;
import org.springframework.web.servlet.config.annotation.CorsRegistry;
import org.springframework.web.servlet.config.annotation.WebMvcConfigurer;

@Configuration
public class WebConfig implements WebMvcConfigurer
{
	@Override
	public void addCorsMappings(CorsRegistry registry) {
		registry.addMapping("/**") // Cho phép tất cả các đường dẫn
				  .allowedOrigins("http://localhost:4321") // Cho phép nguồn gốc của client
				  .allowedMethods("GET", "POST", "PUT", "DELETE") // Cho phép các phương thức HTTP
				  .allowedHeaders("*") // Cho phép tất cả các header
				  .allowCredentials(true); // Cho phép gửi cookie
	}

}
