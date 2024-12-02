package com.example.project.services;

import com.example.project.models.Cart;
import com.example.project.repositories.CartRepository;
import org.springframework.stereotype.Service;

import java.util.Optional;

@Service
public class CartService {

    private final CartRepository cartRepository;

    public CartService(CartRepository cartRepository) {
        this.cartRepository = cartRepository;
    }

    public Optional<Cart> getCartById(String id) {
        return cartRepository.findById(id);
    }

    public Cart addCart(Cart cart) {
        return cartRepository.save(cart);
    }

    public Cart updateCart(String id, Cart cart) {
        cart.setId(id);
        return cartRepository.save(cart);
    }

    public void deleteCart(String id) {
        cartRepository.deleteById(id);
    }
}
