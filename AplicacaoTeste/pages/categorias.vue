<template>
  <v-sheet class="mx-auto" width="400">
    <v-card class="mx-auto" max-width="400">
      <v-toolbar color="orange-lighten-1" dark>
        <v-app-bar-nav-icon></v-app-bar-nav-icon>

        <v-toolbar-title>Cadastro de Categorias</v-toolbar-title>
      </v-toolbar>

      <v-form ref="form" class="pa-5">
        <v-text-field
          v-model="categoria.nome"
          :counter="15"
          :rules="nomeRules"
          label="Categoria"
          required
        ></v-text-field>

        <v-text-field
          v-model="categoria.descricao"
          label="Descrição"
        ></v-text-field>

        <div class="d-flex flex-column">
          <v-btn class="mt-4" color="success" block @click="submit">
            Salvar
          </v-btn>
        </div>
      </v-form>

      <v-data-table
        :headers="headers"
        :items="categorias"
        :items-per-page="5"
        class="elevation-1"
      >
      </v-data-table>
    </v-card>
  </v-sheet>
</template>



<script>
export default {
  data: () => ({
    valid: true,

    categoria: {
      nome: "",
      descricao: "",
    },
    nomeRules: [
      (v) => !!v || "Nome é obrigatório",
      (v) =>
        (v && v.length >= 5 && v.length <= 15) ||
        "O Nome precisa de no mínimo 5 e no máximo 15 caracteres",
    ],

    headers: [
      {
        text: "Nome",
        align: "start",
        sortable: false,
        value: "nome",
      },
      { text: "Descrição", value: "descricao" },
      {text:  'Excluir', value: 'uee'}
    ],
    categorias: [],
  }),
  created() {
    this.fetchCategorias();
  },

  methods: {
    async fetchCategorias() {
      try {
        const responseGet = await this.$axios({
          method: "GET",
          url: "https://localhost:7190/Categoria",
        });

        this.categorias = responseGet.data;
        
      } catch (error) {
        console.error("Erro ao buscar categorias:", error);
      }
    },
    async submit() {
      try {
        const response = await this.$axios({
          method: "POST",
          url: "https://localhost:7190/Categoria",
          data: this.categoria,
        });
        
        this.fetchCategorias();

      } catch (error) {
        console.error("Erro ao enviar categoria:", error);
      }
    },
  },
};
</script>

<style></style>
