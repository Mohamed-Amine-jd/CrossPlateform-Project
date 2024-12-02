package com.example.project.controllers;

import com.example.project.models.Command;
import com.example.project.repositories.CommandRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/api/command")
public class CommandController {

    @Autowired
    private CommandRepository commandRepository;

    @GetMapping("/all")
    public List<Command> getAllCommands() {
        System.out.println("Get all commands");
        return commandRepository.findAll();
    }

    @PostMapping("/create")
    public Command createCommand(@RequestBody Command command) {
        System.out.println(command);
        return commandRepository.save(command);
    }

    @PutMapping("/update/{id}")
    public Command updateCommand(@PathVariable String id, @RequestBody Command command) {
        command.setId(id);

        return commandRepository.save(command);
    }

    @DeleteMapping("/delete/{id}")
    public void deleteCommand(@PathVariable String id) {
        commandRepository.deleteById(id);
    }

    @GetMapping("/user/{userId}")
    public List<Command> getCommandsByUserId(@PathVariable String userId) {
        return commandRepository.findByUserId(userId);
    }
}