class Day5 {

    static String PATH = "src/main/resources/Day5.txt"

    static void main(String[] args) {
        Day5 app = new Day5()

        println("${app.stepCount(app.list())}")
        println("${app.advancedCount(app.list())}")
    }

    List<Integer> list() {
        new File(PATH)
                .readLines()
                .collect { it.trim() as int }
                .findAll { it != null }
    }

    int stepCount(List<Integer> list) {
        int steps = 0
        int position = 0

        while (position < list.size()) {
            int instruction = list[position]

            if (instruction == 0) {
                list[position] += 1
            } else {
                list[position] += 1
                position += instruction
            }
            steps++
        }
        return steps
    }


    long advancedCount(List<Integer> list) {
        long steps = 0
        def current = 0

        while (current < list.size() && current >= 0) {
            steps++
            def offset = list[current]
            list[current] += offset >= 3 ? -1 : 1
            current += offset
        }
        return steps
    }
}
