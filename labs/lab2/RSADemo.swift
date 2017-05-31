//
//  main.swift
//  RSACypher
//
//  Created by Gleb Radchenko on 4/2/17.
//  Copyright © 2017 Gleb Radchenko. All rights reserved.
//

import Foundation

class RSACypher {
    
    static let shared = RSACypher()
    private init() {}
    
    func findPrimes(for n: Int) -> [Int] {
        var numbers = Array<Int>()
        var checkTable: [Int: Bool] = [:]
        (2...n).forEach { numbers.append($0) }
        
        for (index, value) in numbers.enumerated() {
            if checkTable[value] == nil { checkTable[value] = true }
            if checkTable[value] == false { continue }
            stride(from: index + value, to: n - 1, by: value).forEach { (index) in
                checkTable[numbers[index]] = false
            }
        }
        
        return checkTable.filter { $0.value }.map { $0.key }.sorted()
    }
    
    func findPrime(euler: Int) -> Int {
        let suitedNumbers = [17, 257, 65537]
        for suited in suitedNumbers {
            if euler % suited != 0 {
                return suited
            }
        }
        
        
        fatalError()
    }
    
    func d(euler: Int, e: Int) -> Int {
        var d = 3
        while (d * e) % euler != 1 {
            d += 1
        }
        return d
    }
    
    func nod(a: Int, b: Int) -> Int {
        return b == 0 ? a : nod(a: b, b: a % b)
    }
    
    struct Keys {
        var e: Int
        var d: Int
        var n: Int
        
        var publicKey: (Int, Int) {
            return (e, n)
        }
        
        var privateKey: (Int, Int) {
            return (d, n)
        }
    }
    
    func keys() -> Keys {
        let primes = findPrimes(for: 1000)
        let p = primes[primes.count - 23]
        let q = primes[primes.count - 7]
        
        let n = p * q
        let eulerFn = (p - 1) * (q - 1)
        let e = findPrime(euler: eulerFn)
        let d = self.d(euler: eulerFn, e: e)
        
        return Keys(e: e, d: d, n: n)
    }
    
    func encode(m: Int, key: Keys) -> Int {
        let publicKey = key.publicKey
        var result = 1
        (1...publicKey.0).forEach { (_) in
            result *= m
            result %= publicKey.1
        }
        return result
    }
    
    func decode(encrypted: Int, key: Keys) -> Int {
        let privateKey = key.privateKey
        var result = 1
        (1...privateKey.0).forEach { (_) in
            result *= encrypted
            result %= privateKey.1
        }
        return result
    }
}

let key = RSACypher.shared.keys()
let encodedM = RSACypher.shared.encode(m: 1234, key: key)
let decodedM = RSACypher.shared.decode(encrypted: encodedM, key: key)

print(decodedM)


