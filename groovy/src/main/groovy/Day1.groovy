class Day1 {

    static String PATH = "src/main/resources/Day1.txt"

    String input(String path) {
        new File(path).text.trim()
    }

    int result(String statement) {
        String appendedStatement = statement + statement[0]
        List<Boolean> flag = new ArrayList<>(appendedStatement.size())

        flag[0] = false
        for (int i = 1; i < appendedStatement.size(); i++) {
            int prev = appendedStatement[i - 1] as int
            int current = appendedStatement[i] as int
            flag[i - 1] = current == prev
        }

        int sum = 0
        for (int i = 0; i < flag.size(); i++) {
            sum += flag[i] ? appendedStatement[i] as int : 0
        }
        return sum
    }

    static void main(String[] args) {
        Day1 a = new Day1()
        String s = a.input(PATH)
        println a.result(s)
    }
}
