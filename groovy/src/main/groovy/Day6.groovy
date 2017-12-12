class Day6 {

    static String PATH = "src/main/resources/Day6.txt"

    static void main(String[] args) {
        Day6 app = new Day6()

        List<Integer> sectors = new File(PATH)
                .readLines()[0]
                .split('\t')
                .collect { it as int }

        int cycles = app.countCycles(sectors.collect())
        println(" Cycles = $cycles")
    }


    int countCycles(List<Integer> sectors) {
        Set<String> allocations = new HashSet<>()
        allocations.add(sectors.join(','))
        int cycles = 0
        boolean done = false
        while (!done) {
            int max = sectors.max()
            int index = sectors.indexOf(max)

            sectors[index] = 0
            for (i in 0..max - 1) {
                index++
                sectors[index % sectors.size()]++
            }

            String s = sectors.join(',')
            done = allocations.contains(s)
            allocations.add(s)
            cycles++
        }
        return cycles
    }
}
