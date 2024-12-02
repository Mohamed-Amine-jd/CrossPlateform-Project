package com.example.project.controllers;

import com.example.project.models.Cart;
import com.example.project.repositories.CartRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/api/cart")
public class CartController {

    @Autowired
    private CartRepository cartRepository;


    @GetMapping("/all")
    public List<Cart> getAllCarts() {
        return cartRepository.findAll();
    }

    @PostMapping("/create")
    public Cart createCart(@RequestBody Cart cart) {
        return cartRepository.save(cart);
    }

    @PutMapping("/update/{id}")
    public Cart updateCart(@PathVariable String id, @RequestBody Cart cart) {
        cart.setId(id);
        return cartRepository.save(cart);
    }

    @DeleteMapping("/delete/{id}")
    public void deleteCart(@PathVariable String id) {
        cartRepository.deleteById(id);
    }

    @GetMapping("/user/{userId}")
    public List<Cart> getCartByUserId(@PathVariable String userId) {
        return cartRepository.findByUserId(userId);
    }
}
