package com.example.project.controllers;

import com.example.project.models.Product;
import com.example.project.repositories.ProductRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.List;
import java.util.Optional;

@RestController
@RequestMapping("/api/product")
public class ProductController {

    @Autowired
    private ProductRepository productRepository;

    @GetMapping("/all")
    public List<Product> getAllProducts() {
        return productRepository.findAll();
    }

    @PostMapping("/create")
    public Product createProduct(@RequestBody Product product) {
        return productRepository.save(product);
    }

    @PutMapping("/update/{id}")
    public Product updateProduct(@PathVariable String id, @RequestBody Product product) {
        product.setId(id);
        return productRepository.save(product);
    }

    @DeleteMapping("/delete/{id}")
    public void deleteProduct(@PathVariable String id) {
        productRepository.deleteById(id);
    }

    @GetMapping("/category/{category}")
    public List<Product> getProductByCategory(@PathVariable String category) {
        return productRepository.findByCategories(category);
    }



    @GetMapping("/id/{id}")
    public Product getProductById(@PathVariable String id) {
        Optional<Product> product = productRepository.findById(id);
        return product.orElse(null);
    }
}