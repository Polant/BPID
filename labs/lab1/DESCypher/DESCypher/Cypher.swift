//
//  DESCypher.swift
//  DESCypher
//
//  Created by GlebRadchenko on 3/13/17.
//  Copyright © 2017 Gleb Rachenko. All rights reserved.
//

import Foundation

class Des {
    
    public static let instance = Des()
    
    public let IP = [58, 50, 42, 34, 26, 18, 10, 2, 60, 52, 44, 36, 28, 20, 12, 4,
                      62, 54, 46, 38, 30, 22, 14, 6, 64, 56, 48, 40, 32, 24, 16, 8,
                      57, 49, 41, 33, 25, 17, 9, 1, 59, 51, 43, 35, 27, 19, 11, 3,
                      61, 53, 45, 37, 29, 21, 13, 5, 63, 55, 47, 39, 31, 23, 15, 7]
    
    public let decodeIP = [40, 8, 48, 16, 56, 24, 64, 32, 39, 7, 47, 15, 55, 23, 63, 31,
                            38, 6, 46, 14, 54, 22, 62, 30, 37, 5, 45, 13, 53, 21, 61, 29,
                            36, 4, 44, 12, 52, 20, 60, 28, 35, 3, 43, 11, 51, 19, 59, 27,
                            34, 2, 42, 10, 50, 18, 58, 26, 33, 1, 41, 9, 49, 17, 57, 25]
    
    public let E = [32, 1, 2, 3, 4, 5,
                     4, 5, 6, 7, 8, 9,
                     8, 9, 10, 11, 12, 13,
                     12, 13, 14, 15, 16, 17,
                     16, 17, 18, 19, 20, 21,
                     20, 21, 22, 23, 24, 25,
                     24, 25, 26, 27, 28, 29,
                     28, 29, 30, 31, 32, 1]
    
    public let arrayS = [["14 4 13 1 2 15 11 8 3 10 6 12 5 9 0 7",
                           "0 15 7 4 14 2 13 1 10 6 12 11 9 5 3 8",
                           "4 1 14 8 13 6 2 11 15 12 9 7 3 10 5 0",
                           "15 12 8 2 4 9 1 7 5 11 3 14 10 0 6 13"],
                          [
                            "15 1 8 14 6 11 3 4 9 7 2 13 12 0 5 10",
                            "3 13 4 7 15 2 8 14 12 0 1 10 6 9 11 5",
                            "0 14 7 11 10 4 13 1 5 8 12 6 9 3 2 15",
                            "13 8 10 1 3 15 4 2 11 6 7 12 0 5 14 9"],
                          
                          ["10 0 9 14 6 3 15 5 1 13 12 7 11 4 2 8",
                           "13 7 0 9 3 4 6 10 2 8 5 14 12 11 15 1",
                           "13 6 4 9 8 15 3 0 11 1 2 12 5 10 14 7",
                           "1 10 13 0 6 9 8 7 4 15 14 3 11 5 2 12"],
                          
                          ["7 13 14 3 0 6 9 10 1 2 8 5 11 12 4 15",
                           "13 8 11 5 6 15 0 3 4 7 2 12 1 10 14 9",
                           "10 6 9 0 12 11 7 13 15 1 3 14 5 2 8 4",
                           "3 15 0 6 10 1 13 8 9 4 5 11 12 7 2 14"],
                          
                          ["2 12 4 1 7 10 11 6 8 5 3 15 13 0 14 9",
                           "14 11 2 12 4 7 13 1 5 0 15 10 3 9 8 6",
                           "4 2 1 11 10 13 7 8 15 9 12 5 6 3 0 14",
                           "11 8 12 7 1 14 2 13 6 15 0 9 10 4 5 3"],
                          
                          ["12 1 10 15 9 2 6 8 0 13 3 4 14 7 5 11",
                           "10 15 4 2 7 12 9 5 6 1 13 14 0 11 3 8",
                           "9 14 15 5 2 8 12 3 7 0 4 10 1 13 11 6",
                           "4 3 2 12 9 5 15 10 11 14 1 7 6 0 8 13"],
                          
                          ["4 11 2 14 15 0 8 13 3 12 9 7 5 10 6 1",
                           "13 0 11 7 4 9 1 10 14 3 5 12 2 15 8 6",
                           "1 4 11 13 12 3 7 14 10 15 6 8 0 5 9 2",
                           "6 11 13 8 1 4 10 7 9 5 0 15 14 2 3 12"],
                          
                          ["13 2 8 4 6 15 11 1 10 9 3 14 5 0 12 7",
                           "1 15 13 8 10 3 7 4 12 5 6 11 0 14 9 2",
                           "7 11 4 1 9 12 14 2 0 6 10 13 15 3 5 8",
                           "2 1 14 7 4 10 8 13 15 12 9 0 3 5 6 11"]
        ].map({$0.map({$0.components(separatedBy: " ")}).map({$0.flatMap({Int($0)})})})
    
    public let P = [16, 7, 20, 21, 29, 12, 28, 17,
                     1, 15, 23, 26, 5, 18, 31, 10,
                     2, 8, 24, 14, 32, 27, 3, 9,
                     19, 13, 30, 6, 22, 11, 4, 25]
    
    
    private init() {}
    
    
    func replaceWithIP(ip: [Int], bits: [UInt8]) -> [UInt8] {
        var swapped = [UInt8]()
        
        for (swapIndex) in ip {
            swapped.append(bits[swapIndex - 1])
        }
        
        return swapped
    }
    
    //MARK: - Key generating
    func extendWithBits(key: [UInt8]) -> [UInt8] {
        var batch = 0
        var result: [UInt8] = []
        
        while batch < 8 {
            var bytes = key[batch * 7...(batch + 1) * 7 - 1]
            let onesCount = bytes.filter({ $0 == 1 }).count
            bytes.append(onesCount % 2 == 0 ? 1 : 0)
            result.append(contentsOf: bytes)
            batch += 1
        }
        
        return result
    }
    
    public let C0table = [57, 49, 41, 33, 25, 17, 9, 1, 58, 50, 42, 34, 26, 18,
                           10, 2, 59, 51, 43, 35, 27, 19, 11, 3, 60, 52, 44, 36]
    
    public let D0table = [63, 55, 47, 39, 31, 23, 15, 7, 62, 54, 46, 38, 30, 22,
                           14, 6, 61, 53, 45, 37, 29, 21, 13, 5, 28, 20, 12, 4]
    
    
    func generateKeys(from primary: [UInt8], encode: Bool) -> [[UInt8]] {
        var keys: [[UInt8]] = []
        let shiftsTable = encode
            ? [1, 1, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 1]
            : [0, 1, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 1]
        
        let validPositions = [14, 17, 11, 24, 1, 5, 3, 28, 15, 6, 21, 10, 23, 19, 12, 4,
                              26, 8, 16, 7, 27, 20, 13, 2, 41, 52, 31, 37, 47, 55, 30, 40,
                              51, 45, 33, 48, 44, 49, 39, 56, 34, 53, 46, 42, 50, 36, 29, 32]

        var C0: [UInt8] = []
        var D0: [UInt8] = []
        
        zip(C0table, D0table).forEach { (cIndex, dIndex) in
            C0.append(primary[cIndex - 1])
            D0.append(primary[dIndex - 1])
        }
        
        var Ci: [[UInt8]] = [C0]
        var Di: [[UInt8]] = [D0]
        
        for round in 1...16 {
            Ci.append(encode
                ? Ci[round - 1].shiftLeft(by: shiftsTable[round - 1])
                : Ci[round - 1].shiftRight(by: shiftsTable[round - 1]))
            
            Di.append(encode
                ? Di[round - 1].shiftLeft(by: shiftsTable[round - 1])
                : Di[round - 1].shiftRight(by: shiftsTable[round - 1]))
        }
        
        Ci.removeFirst()
        Di.removeFirst()
        
        keys = zip(Ci, Di).map({ (c, d) -> [UInt8] in
            let fullKey = c + d
            print("Key bytes: ", fullKey.toBytesArray())
            var validKey: [UInt8] = []
            
            for (swapIndex) in validPositions {
                validKey.append(fullKey[swapIndex - 1])
            }
            
            print(validKey)
            
            return validKey
        })
        
        debugPrint("Keys: ", keys)
        return keys
    }
    
    func feystel(R: [UInt8], key: [UInt8], Pmash: [Int]) -> [UInt8] { //feistel
        
        var extended: [UInt8] = []
        for (swapIndex) in E {
            extended.append(R[swapIndex - 1])
        }//checked
        
        var xored: [UInt8] = extended ^^ key //checked
        var blocks: [[UInt8]] = []
        
        var blockIndex = 0
        
        while blockIndex < 8 {
            let blockRange = blockIndex * 6...(blockIndex + 1) * 6 - 1
            debugPrint("Processing block in range: ", blockRange)
            let block = xored[blockRange].map { $0 }
            blocks.append(block)
            blockIndex += 1
        }
        
        func getA(block: [UInt8]) -> Int {
            let value = block[0] * UInt8(2.pow(times: 1)) +  block[5] * UInt8(2.pow(times: 0)) // checked

            return Int(value)
        }
        
        func getB(block: [UInt8]) -> Int {
            let value =
                block[1] * UInt8(2.pow(times: 3)) +
                block[2] * UInt8(2.pow(times: 2)) +
                block[3] * UInt8(2.pow(times: 1)) +
                block[4] * UInt8(2.pow(times: 0))
            return Int(value)
        }//checked
        
        var bitBlocks4: [[UInt8]] = []
        
        blocks.enumerated().forEach { (index, block) in
            debugPrint("Processing with S", index)
            let S = arrayS[index]
            let a = getA(block: block)
            let b = getB(block: block)
            debugPrint("a:b is ", (a, b))
            let intValue = S[a][b]
            debugPrint("Int value: ", intValue)
            debugPrint("Bit: ", block)
            debugPrint("Bit': ", intValue.first4bits())
            bitBlocks4.append(intValue.first4bits())
        }
        
        let block32bit = bitBlocks4.reduce([], +)
        
        var Pblock: [UInt8] = []
        for (swapIndex) in Pmash {
            Pblock.append(block32bit[swapIndex - 1])
        }
        
        return Pblock
    }
    
    func encodingRounds(block: [UInt8], keys: [[UInt8]]) -> [UInt8] {
        let L0 = block[0..<32].map { $0 }
        let R0 = block[32..<64].map { $0 }
        
        var Li = L0
        var Ri = R0
        
        for round in 1...16 {
            let RiMinusOne = Ri
            let LiMinusOne = Li
            
            Li = Ri
            Ri = LiMinusOne ^^ feystel(R: RiMinusOne, key: keys[round - 1], Pmash: P)
        }
        
        return Li + Ri
    }
    
    func decodingRounds(block: [UInt8], keys: [[UInt8]]) -> [UInt8] {
        let L0 = block[0..<32].map { $0 }
        let R0 = block[32..<64].map { $0 }
        
        var Li = L0
        var Ri = R0
        
        for round in 1...16 {
            let RiMinusOne = Ri
            let LiMinusOne = Li
            
            Ri = Li
            Li = RiMinusOne ^^ feystel(R: LiMinusOne, key: keys[round - 1], Pmash: P)
        }
        
        return Li + Ri
    }
    
    public func encryptDES(data: DataDecodable, key: String) -> [UInt8] {
        let blocks = data.data().bits().to64Blocks()
        let ipReplacedData = blocks.map({ replaceWithIP(ip: IP, bits: $0) })
        
        let key = key.data(using: .utf8)!.bits()
        assert(key.count == 56, "Wrong key")
        let extendedKey = extendWithBits(key: key)
        let roundKeys = generateKeys(from: extendedKey, encode: true)
        
        //MARK: - Algo
        return ipReplacedData
            .flatMap { (block) -> [UInt8] in
                return replaceWithIP(ip: decodeIP, bits: encodingRounds(block: block, keys: roundKeys))
            }.reduce([], +)
    }
    
    
    
    public func decryptDES(_ encryptedData: [UInt8], key: String) -> [UInt8] {
        let blocks = encryptedData.to64Blocks()
        let ipReplacedData = blocks.map({ replaceWithIP(ip: IP, bits: $0) })
        
        let key = key.data(using: .utf8)!.bits()
        assert(key.count == 56, "Wrong key")
        let extendedKey = extendWithBits(key: key)
        let roundKeys = generateKeys(from: extendedKey, encode: false)
        
        
        //MARK: - Algo
        return ipReplacedData
            .flatMap { (block) -> [UInt8] in
                return replaceWithIP(ip: decodeIP, bits: decodingRounds(block: block, keys: roundKeys))
            }.reduce([], +)
    }
}

