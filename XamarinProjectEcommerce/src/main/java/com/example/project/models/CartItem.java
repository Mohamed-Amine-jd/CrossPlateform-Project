package com.example.project.models;



import org.springframework.data.annotation.Id;
import org.springframework.data.mongodb.core.mapping.Document;
import org.springframework.data.mongodb.core.mapping.DBRef;

@Document(collection = "cart_items") // Spécifie la collection MongoDB
public class CartItem {

    @Id
    private String id; // Utilisation de String pour l'ID (MongoDB utilise ObjectId comme clé par défaut)

    @DBRef
    private Product product; // Référence au document Product

    private Integer quantity;

    // Constructeur par défaut
    public CartItem() {
    }

    // Constructeur paramétré
    public CartItem(Product product, Integer quantity) {
        this.product = product;
        this.quantity = quantity;
    }

    // Getters et Setters
    public String getId() {
        return id;
    }

    public void setId(String id) {
        this.id = id;
    }

    public Product getProduct() {
        return product;
    }

    public void setProduct(Product product) {
        this.product = product;
    }

    public Integer getQuantity() {
        return quantity;
    }

    public void setQuantity(Integer quantity) {
        this.quantity = quantity;
    }
}
