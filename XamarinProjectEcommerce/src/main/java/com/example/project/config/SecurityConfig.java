    package com.example.project.config;

    import org.springframework.context.annotation.Bean;
    import org.springframework.context.annotation.Configuration;
    import org.springframework.security.authentication.AuthenticationManager;
    import org.springframework.security.config.annotation.authentication.builders.AuthenticationManagerBuilder;
    import org.springframework.security.config.annotation.web.builders.HttpSecurity;
    import org.springframework.security.config.annotation.web.configuration.EnableWebSecurity;
    import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
    import org.springframework.security.crypto.password.PasswordEncoder;
    import org.springframework.security.web.SecurityFilterChain;
    import org.springframework.web.cors.CorsConfiguration;
    import org.springframework.web.cors.UrlBasedCorsConfigurationSource;
    import org.springframework.web.filter.CorsFilter;

    @Configuration
    @EnableWebSecurity
    public class SecurityConfig {

        // Configure the HTTP Security settings
        @Bean
        public SecurityFilterChain securityFilterChain(HttpSecurity http) throws Exception {
            http.csrf().disable() // Disable CSRF protection
                    .cors().and() // Enable CORS
                    .authorizeHttpRequests()
                    .requestMatchers("/api/auth/**", "/api/product/**" , "/api/user/**" , "/api/command/**","/api/categories/**","/api/cart/**").permitAll() // Allow authentication endpoints
                    .anyRequest().authenticated() // Protect all other endpoints
                    .and()
                    .formLogin().disable(); // Disable form login for API-based authentication (JWT)

            return http.build();
        }

        // Password encoder bean to encrypt passwords
        @Bean
        public PasswordEncoder passwordEncoder() {
            return new BCryptPasswordEncoder();
        }

        // AuthenticationManager bean for JWT Authentication
        @Bean
        public AuthenticationManager authenticationManager(HttpSecurity http) throws Exception {
            return http.getSharedObject(AuthenticationManagerBuilder.class).build();
        }

        // CORS filter bean
        @Bean
        public CorsFilter corsFilter() {
            UrlBasedCorsConfigurationSource source = new UrlBasedCorsConfigurationSource();
            CorsConfiguration config = new CorsConfiguration();

            config.setAllowCredentials(true);
            config.addAllowedHeader("*");
            config.addAllowedMethod("*");

            // Add allowed origins for emulator, simulator, or devices
            config.addAllowedOrigin("http://10.0.2.2:8080");  // Android Emulator
            config.addAllowedOrigin("http://172.20.10.4:8080");  // Replace with your computer's IP for physical devices
            config.addAllowedOrigin("http://127.0.0.1:8080");  // iOS Simulator

            source.registerCorsConfiguration("/**", config);
            return new CorsFilter(source);
        }


    }