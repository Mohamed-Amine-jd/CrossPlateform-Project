package com.example.project.models;

public class UserStats {
    private int id;
    private int total;

    public UserStats(int id, int total) {
        this.id = id;
        this.total = total;
    }

    public UserStats() {
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public int getTotal() {
        return total;
    }

    public void setTotal(int total) {
        this.total = total;
    }
}
