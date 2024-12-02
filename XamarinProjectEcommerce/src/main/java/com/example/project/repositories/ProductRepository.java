package com.example.project.repositories;

import com.example.project.models.Product;
import org.springframework.data.mongodb.repository.MongoRepository;

import java.util.List;

public interface ProductRepository extends MongoRepository<Product, String> {
    List<Product> findByCategories(String category);// Use the correct field name
}