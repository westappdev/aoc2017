class Day4 {

    static void main(String[] args) {
        Day4 app = new Day4()
        File f = new File("src/main/resources/Day4.txt")

        int count = 0
        int advancedCount = 0
        f.readLines().each {
            if (app.valid(it)) {
                count += 1
            }

            if (app.advancedValid(it)) {
                advancedCount += 1
            }
        }

        println("Count of valid phrases: $count")
        println("Advanced count of valid phrases: $advancedCount")
    }

    boolean valid(String pass) {
        List<String> words = pass.split()
                .collect { it.trim() }
        Set<String> setWords = new HashSet<>(words)
        return words.size() == setWords.size()
    }

    boolean advancedValid(String pass) {
        List<String> words = pass.split()
                .collect { it.trim() }
                .collect { it.split('').sort().join() }
        Set<String> setWords = new HashSet<>(words)
        return words.size() == setWords.size()
    }
}
