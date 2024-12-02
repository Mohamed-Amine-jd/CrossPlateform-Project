package com.example.project.models;

import lombok.Data;
import org.springframework.data.annotation.Id;
import org.springframework.data.mongodb.core.mapping.Document;

import java.util.List;

@Data
@Document(collection = "commands")
public class Command {
    @Id
    private String id;
    private String userId;
    private List<Product> products;
    private double amount;
    private String address;
    private String status = "pending";


    @Data
    public static class Product {
        private String productId;
        private String productName;
        private int quantity = 1;
    }
}