package com.example.project.controllers;

import com.example.project.models.User;
import com.example.project.models.UserStats;
import com.example.project.repositories.UserRepository;
import com.example.project.utils.JwtTokenProvider;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.web.bind.annotation.*;

import java.util.List;
import java.util.Optional;

@RestController
@RequestMapping("/api/user")
public class UserController {

    @Autowired
    private UserRepository userRepository;

    @Autowired
    private JwtTokenProvider jwtTokenProvider;

    // Add a new user
    @PostMapping("/add")
    public ResponseEntity<String> addUser(@RequestBody User user) {
        BCryptPasswordEncoder passwordEncoder = new BCryptPasswordEncoder();
        user.setPassword(passwordEncoder.encode(user.getPassword()));
        userRepository.save(user);
        return ResponseEntity.ok("User registered successfully");
    }

    //get all users

    @GetMapping("/all")
    public ResponseEntity<?> getAllUsers() {
        return ResponseEntity.ok(userRepository.findAll());
    }




    // Update user details (e.g., changing password)
    @PutMapping("/update/{id}")
    public ResponseEntity<String> updateUser(@PathVariable String id, @RequestBody User userDetails) {
        User existingUser = userRepository.findById(id).orElse(null);
        if (existingUser == null) {
            return ResponseEntity.status(404).body("User not found");
        }

        existingUser.setEmail(userDetails.getEmail());
        existingUser.setPassword(new BCryptPasswordEncoder().encode(userDetails.getPassword()));
        userRepository.save(existingUser);

        return ResponseEntity.ok("User updated successfully");
    }

    // Delete a user
    @DeleteMapping("/delete/{id}")
    public ResponseEntity<String> deleteUser(@PathVariable String id) {
        userRepository.deleteById(id);
        return ResponseEntity.ok("User deleted successfully");
    }


    @GetMapping("/userStats")
    public ResponseEntity<List<UserStats>> getUserStats() {
        return ResponseEntity.ok(userRepository.getUserStats());
    }

}