package com.example.project.services;

import com.example.project.models.MonthlyIncome;
import com.example.project.repositories.CommandRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class CommandService {

    @Autowired
    private CommandRepository commandRepository;

    public List<MonthlyIncome> getMonthlyIncome() {
        return commandRepository.getMonthlyIncome();
    }
}