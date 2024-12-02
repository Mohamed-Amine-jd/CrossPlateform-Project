package com.example.project.models;

public class MonthlyIncome {
    private int _id;
    private double total;

    public MonthlyIncome(int _id, double total) {
        this._id = _id;
        this.total = total;
    }

    public MonthlyIncome() {
    }

    public int get_id() {
        return _id;
    }

    public void set_id(int _id) {
        this._id = _id;
    }

    public double getTotal() {
        return total;
    }

    public void setTotal(double total) {
        this.total = total;
    }
}