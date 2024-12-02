package com.example.project.repositories;

import com.example.project.models.User;
import com.example.project.models.UserStats;
import org.springframework.data.mongodb.repository.Aggregation;
import org.springframework.data.mongodb.repository.MongoRepository;
import org.springframework.stereotype.Repository;

import java.util.List;
import java.util.Optional;

@Repository
public interface UserRepository extends MongoRepository<User, String> {
    Optional<User> findByEmail(String email);

    @Aggregation(pipeline = {
            "{ '$group': { 'id': { '$month': '$createdAt' }, 'total': { '$sum': 1 } } }",
            "{ '$sort': { 'id': 1 } }"
    })
    List<UserStats> getUserStats();
}