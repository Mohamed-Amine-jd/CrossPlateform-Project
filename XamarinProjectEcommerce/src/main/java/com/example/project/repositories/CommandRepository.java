package com.example.project.repositories;

import com.example.project.models.Command;
import com.example.project.models.MonthlyIncome;
import org.springframework.data.mongodb.repository.Aggregation;
import org.springframework.data.mongodb.repository.MongoRepository;
import org.springframework.stereotype.Repository;

import java.util.List;

@Repository
public interface CommandRepository extends MongoRepository<Command, String> {
    List<Command> findByUserId(String userId);

    @Aggregation(pipeline = {
            "{ '$group': { '_id': { '$month': '$createdAt' }, 'total': { '$sum': '$totalAmount' } } }",
            "{ '$sort': { '_id': 1 } }"
    })
    List<MonthlyIncome> getMonthlyIncome();
}