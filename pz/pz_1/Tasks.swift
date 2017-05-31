//MARK: - XOR using AND(&) OR(|) NOT(~)
let a: UInt8 = 0b0110
let b: UInt8 = 0b1001

~a //All except a (including B without a part)
~a & b // previous intersect with b - gives b part without a part
~b & a // the same : a part withot B part
(~a & b) | (~b & a) // excluding OR - XOR (A and B without AB part)

let xor: (Int, Int) -> Int = { a, b in
    return (~a & b) | (~b & a)
}

func testXor() {
    for a in 1...10 {
        for b in 1...10 {
            let native = a ^ b
            let custom = xor(a, b)
            if native != custom {
                print("Test failed")
            }
        }
    }
}

testXor()


//MARK: - Counting of characters
func count(string: String) -> [Character: Int] {
    var countDictionary: [Character: Int] = [:]
    string.characters.forEach { character in
        if countDictionary[character] == nil {
            countDictionary[character] = 0
        }
        countDictionary[character]! += 1
    }
    
    return countDictionary
}

//MARK: - Cesar's encoding
class CesarEncoder {
    var alphabet: [Character]
    var alphabetTable: [Character: Int]
    var shift: Int = 0
    
    init() {
        alphabet = "abcdefghijklmnopqrstuvwxyz".characters.flatMap { $0 }
        alphabetTable = [:]
        alphabet.enumerated().forEach { idx, symbol in
            alphabetTable[symbol] = idx
        }
    }
    
    func encode(initialString: String) -> String {
        let encodedCharacters = initialString
            .lowercased()
            .characters
            .flatMap { (character: Character) -> Character in
                if let initialIndex = alphabetTable[character] {
                    let newIndex = (initialIndex + shift) % alphabet.count
                    return alphabet[newIndex]
                }
                return character
        }
        return String(encodedCharacters)
    }
    
    func decode(encodedString: String, shift: Int) -> String {
        let shift = shift % alphabet.count
        let decodedCharacters = encodedString.lowercased().characters.flatMap { (character: Character) -> Character in
            if let index = alphabetTable[character] {
                let initialIndex = (index - shift) >= 0 ? index - shift : alphabet.count + (index - shift)

                return alphabet[initialIndex]
            }
            return character
        }
        
        return String(decodedCharacters)
    }
}

let encoder = CesarEncoder()
encoder.shift = 231
let encoded = encoder.encode(initialString: "Hello, world")
let decoded = encoder.decode(encodedString: encoded, shift: 231)

//MARK: - One time pad (Vernam cipher)

func encryptDecrypt(input: String, staticKey: String) -> String {
    let key = staticKey.utf8
    
    let bytes = input.utf8.enumerated().map {
        $1 ^ key[key.index(key.startIndex, offsetBy: $0 % key.count)]
    }
    
    return String(bytes: bytes, encoding: String.Encoding.utf8)!
}

let key = "23sdasgg%%%agasgd"
let string = "Hello, World!"
let encrypted = encryptDecrypt(input: string, staticKey: key)
let decrypted = encryptDecrypt(input: encrypted, staticKey: key)

//MARK: - Statistics
func percentageOfChracters(in string: String) -> [Character: Double] {
    let countTable = count(string: string)
    let summaryCount = countTable.values.reduce(0, +)
    
    var percentageTable: [Character: Double] = [:]
    
    countTable.forEach { (char, count) in
        let percentage = Double(count) / Double(summaryCount) * 100
        percentageTable[char] = percentage
    }
    
    return percentageTable
}