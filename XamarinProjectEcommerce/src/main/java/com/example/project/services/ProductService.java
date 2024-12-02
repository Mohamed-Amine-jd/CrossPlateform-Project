package com.example.project.services;

import com.example.project.models.Product;
import com.example.project.repositories.ProductRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Optional;

@Service
public class ProductService {

    @Autowired
    private ProductRepository productRepository;

    public List<Product> getAllProducts() {
        return productRepository.findAll();
    }

    public Product addProduct(Product product) {
        return productRepository.save(product);
    }

    public Product updateProduct(String id, Product product) {
        product.setId(id);
        return productRepository.save(product);
    }

    public void deleteProduct(String id) {
        productRepository.deleteById(id);
    }

    public List<Product> getProductByCategory(String category) {
        return productRepository.findByCategories(category);
    }

    public Product getProductById(String id) {
        Optional<Product> product = productRepository.findById(id);
        return product.orElse(null);
    }
}