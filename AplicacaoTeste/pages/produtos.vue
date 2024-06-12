<template>
  <v-sheet class="mx-auto" width="400">
    <v-card class="mx-auto" max-width="400">
      <v-toolbar color="orange-lighten-1" dark>
        <v-app-bar-nav-icon></v-app-bar-nav-icon>

        <v-toolbar-title>Cadastro de Produtos</v-toolbar-title>
      </v-toolbar>

      <v-form ref="form" class="pa-5">
        <v-text-field
          v-model="produto.nome"
          :counter="20"
          :rules="nomeRules"
          label="Produto"
          required
        ></v-text-field>

        <v-text-field
          v-model="produto.descricao"
          label="Descrição"
        ></v-text-field>

        <v-select
          v-model="produto.categoriaId"
          :items="categorias"
          label="Categoria"
          :rules="categoriaRules"
          item-text="nome"
          item-value="id"
        ></v-select>

        <div class="d-flex flex-column">
          <v-btn class="mt-4" color="success" block @click="submit">
            Salvar
          </v-btn>
        </div>

      </v-form>

      <v-data-table
        :headers="headers"
        :items="produtos"
        :items-per-page="5"
        class="elevation-1"
      >
        <template v-slot:item.categoriaId="{ item }">
          {{ getCategoriaNome(item.categoriaId) }}
        </template>
        
        <template v-slot:item.dataCadastrada="{ item }">
          {{ formatData(item.dataCadastrada) }}
        </template>
      </v-data-table>
    </v-card>
  </v-sheet>
</template>

<script>

export default {
  data: () => ({
    valid: true,
    produto: {
      nome: "",
      descricao: "",
      categoriaId: null,
    },
    nomeRules: [
      (v) => !!v || "O nome do produto é obrigatório",
      (v) =>
        (v && v.length >= 5 && v.length <= 20) ||
        "O Nome precisa de no mínimo 5 e no máximo 20 caracteres",
    ],
    categoriaRules: [(v) => !!v || "A categoria do produto é obrigatória"],

    headers: [
      {
        text: "Produto",
        align: "start",
        sortable: false,
        value: "nome",
      },
      { text: "Descrição", value: "descricao" },
      { text: "Categoria", value: "categoriaId" },
      { text: "Data", value: "dataCadastrada" },

    ],

    produtos: [],
    categorias: [],
  }),

  async mounted() {
    await this.carregarProdutos();
    await this.carregarCategorias();
  },

  methods: {
    getCategoriaNome(categoriaId) {
      const categoria = this.categorias.find(c => c.id == categoriaId);
      return categoria ? categoria.nome : "";
    },
    formatData(data) {
      const date = new Date(data);
      return `${date.getDate()}/${date.getMonth() + 1}/${date.getFullYear()}`;
    },

    async carregarProdutos() {
      try {
        const response = await fetch('https://localhost:7190/Produto');
        const data = await response.json();
        this.produtos = data;
      } catch (error) {
        console.error('Erro ao buscar produtos:', error);
      }
    },
    async carregarCategorias() {
      try {
        const response = await fetch('https://localhost:7190/Categoria');
        const data = await response.json();
        this.categorias = data;
      } catch (error) {
        console.error('Erro ao buscar categorias:', error);
      }
    },

    async submit() {
      try {
        const response = await this.$axios({
          method: "POST",
          url: "https://localhost:7190/Produto",
          data: this.produto,
        });

        const responseGet = await this.$axios({
          method: "GET",
          url: "https://localhost:7190/Produto",
        });

        this.produtos = responseGet.data;
        
      } catch (error) {
        console.error("Erro ao salvar produto:", error);
      }
    },
  },
};
</script>

<style></style>
