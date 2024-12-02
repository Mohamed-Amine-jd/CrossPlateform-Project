package com.example.project.utils;

import io.jsonwebtoken.Jwts;
import io.jsonwebtoken.SignatureAlgorithm;
import io.jsonwebtoken.security.Keys;
import org.springframework.stereotype.Component;

import javax.crypto.SecretKey;
import java.util.Date;
import java.util.Map;

@Component
public class JwtTokenProvider {

    private static final SecretKey key = Keys.secretKeyFor(SignatureAlgorithm.HS512);

    public String createToken(String username, boolean isAdmin , String userId )
    {return Jwts.builder()
                .setSubject(username)
                .claim("isAdmin", isAdmin)
                .claim("userId", userId)


            .setIssuedAt(new Date())
                .setExpiration(new Date(System.currentTimeMillis() + 86400000)) // 24 hours
                .signWith(key)
                .compact();
    }
}