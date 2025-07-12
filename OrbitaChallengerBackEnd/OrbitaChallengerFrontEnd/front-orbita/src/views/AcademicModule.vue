<template>
  <LoggedLayout>
    <v-app>
      <v-col>
        <v-card class="mb-4">
          <v-card-title>Consulta de alunos</v-card-title>
          <v-card-text>
            <v-row>
              <v-col cols="8">
                <v-text-field label="Digite sua busca" v-model="search" dense></v-text-field>
              </v-col>
              <v-col cols="2">
                <v-btn color="#696969" block @click="searchStudent">Pesquisar</v-btn>
              </v-col>
              <v-col cols="2">
                <v-btn color="#696969" block @click="createStudent">Cadastrar</v-btn>
              </v-col>
            </v-row>
          </v-card-text>
        </v-card>

        <v-container>
            <v-row>
                <v-col cols="12">
                <v-data-table
                    :headers="headers"
                    :items="alunos"
                    class="elevation-1"
                    :items-per-page="10"
                    fixed-header
                    height="400"
                    :loading="alunos.length === 0"
                    loading-text="Carregando alunos..."
                    no-data-text="Nenhum aluno encontrado."
                >
                    <template v-slot:item.actions="{ item }">
                    <v-btn small text color="#696969" @click="editStudent(item)">
                        <v-icon left>mdi-pencil</v-icon>
                        Editar
                    </v-btn>

                    <v-btn small text color="#696969" @click="openConfirmationDialog(item)">
                        <v-icon left>mdi-delete</v-icon>
                        Excluir
                    </v-btn>
                    </template>
                </v-data-table>
                </v-col>
            </v-row>
            </v-container>


        <ConfirmationDialog
          :student="clickedStudent"
          :visible="dialog"
          :title="'Confirmar Exclusão'"
          :content="`Tem certeza que deseja excluir o aluno com o RA: ${clickedStudent?.ra}?`"
          :cancelText="'Cancelar'"
          :confirmText="'Excluir'"
          @confirm="() => deleteStudent(clickedStudent)"
          @close="dialog = false"
        />

        <SnackBar
          v-model:visible="snackbarVisible"
          :message="snackbarMessage"
          :isSuccess="snackbarIsSuccess"
        />
      </v-col>
    </v-app>
  </LoggedLayout>
</template>

<script>
import { getAllStudents, getByName, deleteStudentByRA } from '@/api';
import SnackBar from '@/components/SnackBar.vue';
import LoggedLayout from '@/components/LoggedLayout.vue';
import ConfirmationDialog from '@/components/ConfirmationDialog.vue';

export default {
  components: {
    SnackBar,
    LoggedLayout,
    ConfirmationDialog,
  },
  data() {
    return {
      search: '',
      alunos: [],
      headers: [
        { text: 'Registro Acadêmico', value: 'ra', align: 'center' },
        { text: 'Nome', value: 'name', align: 'center' },
        { text: 'CPF', value: 'cpf', align: 'center' },
        { text: 'Ações', value: 'actions', sortable: false, align: 'center' },
        ],  
      clickedStudent: null,
      dialog: false,
      snackbarVisible: false,
      snackbarMessage: '',
      snackbarIsSuccess: true,
    };
  },
  methods: {
    createStudent() {
      this.$router.push({ name: 'AddStudent' });
    },
    openConfirmationDialog(student) {
      this.clickedStudent = student;
      this.dialog = true;
    },
    editStudent(student) {
      this.$router.push({ name: 'EditStudent', params: { id: student.ra } });
    },
    async deleteStudent(student) {
      try {
        const response = await deleteStudentByRA(student.ra);
        if (response.data.success) {
          this.snackbarMessage = response.data.message;
          this.snackbarIsSuccess = true;
          this.snackbarVisible = true;
          this.dialog = false;
          await this.fetchStudents();
        } else {
          this.snackbarMessage = response.data.message;
          this.snackbarIsSuccess = false;
          this.snackbarVisible = true;
        }
      } catch (error) {
        this.snackbarMessage = error.message || error;
        this.snackbarIsSuccess = false;
        this.snackbarVisible = true;
      }
    },
    async searchStudent() {
      if (this.search.trim()) {
        try {
          const response = await getByName(this.search.trim());
          if (response.data.success) {
            this.alunos = response.data.data;
          } else {
            this.alunos = [];
            this.snackbarMessage = response.data.message;
            this.snackbarIsSuccess = false;
            this.snackbarVisible = true;
          }
        } catch (error) {
          this.alunos = [];
          this.snackbarMessage = error.message || error;
          this.snackbarIsSuccess = false;
          this.snackbarVisible = true;
        }
      } else {
        this.fetchStudents();
      }
    },
    async fetchStudents() {
      try {
        const response = await getAllStudents();
        if (response.data.success) {
          this.alunos = response.data.data;
        } else {
          this.alunos = [];
          this.snackbarMessage = response.data.message || 'Erro ao buscar alunos.';
          this.snackbarIsSuccess = false;
          this.snackbarVisible = true;
        }
      } catch (error) {
        this.snackbarMessage = error.message || error;
        this.snackbarIsSuccess = false;
        this.snackbarVisible = true;
      }
    },
  },
  mounted() {
    this.fetchStudents();
  },
};
</script>

<style scoped></style>
